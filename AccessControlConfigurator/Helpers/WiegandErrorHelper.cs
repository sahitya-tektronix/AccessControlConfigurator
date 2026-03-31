using System;
using System.Text.Json;

namespace AccessControlConfigurator.Helpers
{
    internal static class WiegandErrorHelper
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
                "invalid_format_number" => "Format number must be between 0 and 7.",
                "invalid_format_bits" => "Bits must be greater than zero.",
                "invalid_facility_code" => "Facility code must be -1 (not used) or a non-negative value.",
                "wiegand_format_not_found" => "Format not found.",
                "duplicate_format_number" => "Format number already exists.",
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
