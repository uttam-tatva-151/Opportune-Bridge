# RefreshToken Table

The `RefreshToken` table is a vital component of a secure authentication system. It is designed to manage refresh tokens, which allow users to obtain new access tokens without needing to log in repeatedly. This improves user experience while maintaining robust security.

## Purpose of the RefreshToken Table

### 1. Session Management
- Tracks user sessions across devices and applications.
- Enables users to stay logged in for extended periods without re-entering credentials.

### 2. Token Rotation
- Facilitates secure token rotation by replacing old refresh tokens with new ones.
- Prevents the reuse of compromised or expired tokens.

### 3. Revocation and Auditing
- Provides mechanisms to revoke tokens in case of suspicious activity or user logout.
- Tracks token usage for auditing, debugging, and security analysis.

---

## Fields in the RefreshToken Table

| **Field Name**     | **Data Type**   | **Purpose**                                                                 | **Use Cases**                                                                 |
|---------------------|-----------------|-----------------------------------------------------------------------------|-------------------------------------------------------------------------------|
| `Id`               | UUID            | Unique identifier for each refresh token.                                   | Used for database operations like updates and deletions.                     |
| `UserId`           | UUID            | Links the refresh token to a specific user.                                 | Ensures tokens are associated with the correct user account.                 |
| `Token`            | TEXT            | The actual refresh token string (securely generated).                       | Used to authenticate requests for new access tokens.                         |
| `ExpiresAt`        | TIMESTAMP       | The expiration date and time of the refresh token.                          | Ensures tokens are invalidated after a specific period.                      |
| `CreatedAt`        | TIMESTAMP       | The date and time when the refresh token was created.                       | Tracks when the token was issued.                                            |
| `CreatedByIp`      | VARCHAR(50)     | The IP address of the client where the token was created.                   | Helps detect unusual token creation locations and identify suspicious activity. |
| `UserAgent`        | VARCHAR(500)    | The user agent string of the client (e.g., browser or app details).         | Tracks the device or application used to create the token.                   |
| `DeviceId`         | VARCHAR(255)    | A unique identifier for the user's device (e.g., UUID).                     | Helps manage tokens for specific devices and sessions.                       |
| `IsRevoked`        | BOOLEAN         | Indicates whether the token has been explicitly revoked.                    | Prevents the use of revoked tokens.                                          |
| `RevokedAt`        | TIMESTAMP       | The date and time when the token was revoked.                               | Tracks when the token was revoked for auditing purposes.                     |
| `RevokedByIp`      | VARCHAR(50)     | The IP address of the client where the token was revoked.                   | Identifies the origin of the revocation request and helps detect anomalies.  |
| `IsActive`         | BOOLEAN         | Indicates whether the token is currently active (not expired or revoked).   | Simplifies checks for token validity.                                        |

---

## Why Each Field is Needed

### 1. `Id`
- **Purpose**: Uniquely identifies each refresh token.
- **Use Case**: Required for database operations like updates and deletions.

### 2. `UserId`
- **Purpose**: Links the token to a specific user.
- **Use Case**: Ensures that tokens are associated with the correct user account.

### 3. `Token`
- **Purpose**: Stores the actual refresh token string.
- **Use Case**: Used to authenticate requests for new access tokens.

### 4. `ExpiresAt`
- **Purpose**: Specifies when the token will expire.
- **Use Case**: Ensures tokens are invalidated after a specific period to enhance security.

### 5. `CreatedAt`
- **Purpose**: Tracks when the token was issued.
- **Use Case**: Useful for auditing and debugging.

### 6. `CreatedByIp`
- **Purpose**: Logs the IP address where the token was created.
- **Use Case**: 
    - Detects suspicious activity, such as token creation from unusual locations.
    - Helps enforce geolocation-based security policies.
    - Enables tracking of token creation patterns for enhanced security.

### 7. `UserAgent`
- **Purpose**: Tracks the client details (e.g., browser, operating system).
- **Use Case**: Helps identify the device or application used to create the token.

### 8. `DeviceId`
- **Purpose**: Identifies the user's device.
- **Use Case**: Enables device-specific session management and token revocation.

### 9. `IsRevoked`
- **Purpose**: Indicates whether the token has been explicitly revoked.
- **Use Case**: Prevents the use of revoked tokens.

### 10. `RevokedAt`
- **Purpose**: Tracks when the token was revoked.
- **Use Case**: Useful for auditing and debugging.

### 11. `RevokedByIp`
- **Purpose**: Logs the IP address where the token was revoked.
- **Use Case**: 
    - Identifies the origin of the revocation request.
    - Detects suspicious revocation attempts from unknown or unusual IP addresses.
    - Helps enforce IP-based security policies.

### 12. `IsActive`
- **Purpose**: Indicates whether the token is currently active.
- **Use Case**: Simplifies checks for token validity.

---

## Use Cases for Each Field

### 1. Token Creation
- **Fields Used**: `UserId`, `Token`, `ExpiresAt`, `CreatedAt`, `CreatedByIp`, `UserAgent`, `DeviceId`.
- **Scenario**: A user logs in, and a refresh token is created to maintain their session.

### 2. Token Revocation
- **Fields Used**: `IsRevoked`, `RevokedAt`, `RevokedByIp`.
- **Scenario**: A user logs out or an admin revokes a token due to suspicious activity.

### 3. Auditing and Debugging
- **Fields Used**: `CreatedByIp`, `RevokedByIp`.
- **Scenario**: An admin investigates suspicious activity by reviewing token creation and usage logs.

### 4. Device-Specific Session Management
- **Fields Used**: `DeviceId`, `UserAgent`.
- **Scenario**: A user views and manages active sessions across multiple devices.

---

## Security Benefits of These Fields

### 1. IP Tracking
- Tracks the origin of token creation and revocation to detect suspicious activity.
- Helps enforce geolocation-based security policies.
- Identifies unusual patterns, such as multiple tokens created from different IPs in a short time.

### 2. Device Management
- Enables users to manage sessions on specific devices.
- Allows administrators to revoke tokens for compromised devices.

### 3. Token Rotation
- Prevents reuse of old tokens, reducing the risk of token theft.

### 4. Auditing
- Provides detailed logs for investigating security incidents.

### 5. Expiration and Revocation
- Ensures tokens are invalidated after a specific period or upon user logout.

---

## Conclusion

The `RefreshToken` table is a cornerstone of a secure authentication system. Each field is carefully designed to enhance security, improve user experience, and provide robust mechanisms for auditing and debugging. By leveraging these fields effectively, organizations can ensure a secure and seamless authentication process.