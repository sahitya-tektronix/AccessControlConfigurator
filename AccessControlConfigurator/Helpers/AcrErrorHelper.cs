using System;
using System.Text.Json;

namespace AccessControlConfigurator.Helpers
{
    internal static class AcrErrorHelper
    {
        public static string GetMessage(Exception ex)
        {
            if (ex == null)
                return "Unexpected error.";

            return GetMessage(ex.Message);
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
                "controller_not_found" => "Controller not found.",
                "sio_not_found" => "SIO not found for the specified controller.",
                "acr_not_found" => "ACR not found.",
                "acr_number_in_use" => "The specified ACR number is already in use for this controller and SIO.",
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
