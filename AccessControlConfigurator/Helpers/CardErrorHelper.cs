using System;
using System.Text.Json;

namespace AccessControlConfigurator.Helpers
{
    internal static class CardErrorHelper
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
                "invalid_card_number" => "Card number must be greater than zero.",
                "invalid_card_time_range" => "Invalid card time range.",
                "card_not_found" => "Card not found.",
                "cardholder_not_found" => "Cardholder not found.",
                "duplicate_card_number" => "Card number already exists.",
                "card_already_assigned" => "Card is already assigned to another active cardholder.",
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
