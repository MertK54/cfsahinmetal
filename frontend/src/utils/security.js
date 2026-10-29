/**
 * Security & Input Validation Helpers
 * Provides XSS sanitization, SQL Injection keyword checks, and phone input masking.
 */

// SQL Injection and XSS detection regex
const SQL_XSS_REGEX = /(<[^>]*>|--|;|\/\*|\*\/|@@|char\s*\(|nchar\s*\(|alter\s+table|delete\s+from|drop\s+table|insert\s+into|select\s+.*\s+from|union\s+select)/i

/**
 * Sanitizes a string by stripping HTML tags, control characters, and trimming.
 */
export function sanitizeInput(input, maxLength = 2000) {
  if (!input || typeof input !== 'string') return ''
  
  // 1. Remove HTML tags
  let clean = input.replace(/<[^>]*>?/gm, '')
  
  // 2. Remove null bytes and control characters
  clean = clean.replace(/[\x00-\x08\x0B\x0C\x0E-\x1F]/g, '')
  
  // 3. Trim whitespace
  clean = clean.trim()
  
  // 4. Enforce max length
  if (clean.length > maxLength) {
    clean = clean.substring(0, maxLength)
  }
  
  return clean
}

/**
 * Checks if the string contains obvious SQL injection or script injection signatures.
 */
export function hasDangerousPayload(input) {
  if (!input || typeof input !== 'string') return false
  return SQL_XSS_REGEX.test(input)
}

/**
 * Formats Turkish phone number digits: 05XX XXX XX XX
 */
export function formatPhoneNumber(value) {
  if (!value) return ''
  
  // Extract only digits
  let digits = value.replace(/\D/g, '')
  
  // Ensure starts with 0 if starts with 5
  if (digits.startsWith('5')) {
    digits = '0' + digits
  } else if (digits.startsWith('905')) {
    digits = '0' + digits.substring(2)
  }
  
  // Limit to 11 digits (e.g. 05321234567)
  digits = digits.substring(0, 11)
  
  // Format as 05XX XXX XX XX
  if (digits.length <= 4) {
    return digits
  } else if (digits.length <= 7) {
    return `${digits.slice(0, 4)} ${digits.slice(4)}`
  } else if (digits.length <= 9) {
    return `${digits.slice(0, 4)} ${digits.slice(4, 7)} ${digits.slice(7)}`
  } else {
    return `${digits.slice(0, 4)} ${digits.slice(4, 7)} ${digits.slice(7, 9)} ${digits.slice(9, 11)}`
  }
}
