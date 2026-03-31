using System;
using System.Text.Json;

namespace AccessControlConfigurator.Helpers
{
    internal static class ControllerErrorHelper
    {
        public static string GetMessage(Exception ex)
        {
            if (ex == null)
                return "Unexpected error.";

            return GetMessage(ex.Message);
        }

        public static string GetMessage(Exception ex, string fallbackMessage)
        {
            if (ex == null)
                return string.IsNullOrWhiteSpace(fallbackMessage) ? "Unexpected error." : fallbackMessage;

            var message = GetMessage(ex.Message);
            return string.IsNullOrWhiteSpace(message) ? fallbackMessage : message;
        }

        public static string GetMessage(string rawMessage)
        {
            if (string.IsNullOrWhiteSpace(rawMessage))
                return "Unexpected error.";

            if (!TryParseProblemDetails(rawMessage, out var errorCode, out var detail, out var title))
                return rawMessage;

            return errorCode switch
            {
                "request_body_required" => "Request body is required.",
                "controller_name_required" => "Controller name is required.",
                "controller_mac_address_required" => "MAC address is required.",
                "controller_id_mismatch" => "Route id does not match request body id.",
                "invalid_controller_id" => "Controller id must be greater than zero.",
                "invalid_scp_id" => "SCP id must be greater than zero.",
                "invalid_ip_address" => "IP address is required and must be valid.",
                "duplicate_mac_address" => "MAC address already exists.",
                "controller_limit_exceeded" => "Only one enabled controller is allowed.",
                "controller_already_enabled" => "Controller is already enabled.",
                "controller_not_found" => "Controller not found.",
                _ => !string.IsNullOrWhiteSpace(detail) ? detail : title ?? rawMessage
            };
        }

        private static bool TryParseProblemDetails(
            string rawMessage,
            out string errorCode,
            out string detail,
            out string title)
        {
            errorCode = string.Empty;
            detail = string.Empty;
            title = string.Empty;

            var json = ExtractJson(rawMessage);
            if (string.IsNullOrWhiteSpace(json))
                return false;

            try
            {
                using var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;

                if (root.TryGetProperty("errorCode", out var codeProp))
                    errorCode = codeProp.GetString() ?? string.Empty;

                if (root.TryGetProperty("detail", out var detailProp))
                    detail = detailProp.GetString() ?? string.Empty;

                if (root.TryGetProperty("title", out var titleProp))
                    title = titleProp.GetString() ?? string.Empty;

                if (string.IsNullOrWhiteSpace(errorCode))
                {
                    if (root.TryGetProperty("type", out var typeProp))
                    {
                        var type = typeProp.GetString() ?? string.Empty;
                        var marker = "urn:problem-type:";
                        var idx = type.LastIndexOf(marker, StringComparison.OrdinalIgnoreCase);
                        if (idx >= 0)
                            errorCode = type[(idx + marker.Length)..];
                    }
                }

                return !string.IsNullOrWhiteSpace(errorCode) ||
                       !string.IsNullOrWhiteSpace(detail) ||
                       !string.IsNullOrWhiteSpace(title);
            }
            catch
            {
                return false;
            }
        }

        private static string ExtractJson(string raw)
        {
            var start = raw.IndexOf('{');
            var end = raw.LastIndexOf('}');
            if (start < 0 || end <= start)
                return string.Empty;

            return raw.Substring(start, end - start + 1);
        }
    }
}
