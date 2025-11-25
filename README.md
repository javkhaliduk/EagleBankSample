# EagleBank API

A REST API for Eagle Bank that allows users to manage bank accounts and transactions. Built with .NET 8 and following clean architecture principles.

## Features

- User management (create, read, update, delete)
- Bank account management with account number validation
- Transaction processing (deposits and withdrawals)
- JWT authentication and authorization
- Rate limiting (10 requests per minute)
- In-memory caching for data storage
- Comprehensive unit testing

## Architecture

The application follows a layered architecture:

- **API Layer** (`EagleBank`): Controllers and middleware
- **Orchestration Layer** (`EagleBank.Orchestration`): Business logic coordination
- **Repository Layer** (`EagleBank.Repository`): Data access abstraction
- **Models Layer** (`EagleBank.Models`): DTOs, entities, and shared models

## Prerequisites

- .NET 8 SDK
- Visual Studio 2022 or VS Code

## Setup

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd EagleBank
   ```

2. **Configure JWT Settings**
   
   Update `appsettings.Development.json`:
   ```json
   {
     "JWTSettings": {
       "ValidAudience": "EagleBank",
       "ValidIssuer": "EagleBank",
       "Secret": "your-super-secret-key-minimum-32-characters"
     },
     "CacheDurationMinutes": "30"
   }
   ```

3. **Build the solution**
   ```bash
   dotnet build
   ```

4. **Run the application**
   ```bash
   cd EagleBank
   dotnet run
   ```

The API will be available at `https://localhost:7000` (or check console output for actual port).

## API Endpoints

### Authentication
- **Note**: Authentication endpoint not yet implemented. JWT tokens must be generated manually for testing.

### Users
- `POST /v1/users` - Create a new user
- `GET /v1/users/{userId}` - Get user by ID (requires auth)
- `PATCH /v1/users/{userId}` - Update user (requires auth)
- `DELETE /v1/users/{userId}` - Delete user (requires auth)

### Bank Accounts
- `POST /v1/accounts` - Create bank account (requires auth)
- `GET /v1/accounts` - List all accounts for user (requires auth)
- `GET /v1/accounts/{accountNumber}` - Get account by number (requires auth)
- `PATCH /v1/accounts/{accountNumber}` - Update account (requires auth)
- `DELETE /v1/accounts/{accountNumber}` - Delete account (requires auth)

### Transactions
- `POST /v1/accounts/{accountNumber}/transactions` - Create transaction (requires auth)
- `GET /v1/accounts/{accountNumber}/transactions` - List transactions (requires auth)
- `GET /v1/accounts/{accountNumber}/transactions/{transactionId}` - Get transaction by ID (requires auth)

## Request/Response Examples

### Create User
```json
POST /v1/users
{
  "name": "John Doe",
  "email": "john.doe@example.com",
  "phoneNumber": "+1234567890",
  "address": {
    "line1": "123 Main St",
    "town": "London",
    "county": "Greater London",
    "postcode": "SW1A 1AA"
  }
}
```

### Create Bank Account
```json
POST /v1/accounts
Authorization: Bearer <jwt-token>
{
  "name": "Personal Account",
  "accountType": "personal"
}
```

### Create Transaction
```json
POST /v1/accounts/01234567/transactions
Authorization: Bearer <jwt-token>
{
  "amount": 100.50,
  "currency": "GBP",
  "type": "deposit",
  "reference": "Initial deposit"
}
```

## Validation Rules

- **Account Numbers**: Must follow pattern `01XXXXXX` (8 digits starting with 01)
- **User IDs**: Should follow pattern `usr-[A-Za-z0-9]+`
- **Transaction IDs**: Should follow pattern `tan-[A-Za-z0-9]`
- **Phone Numbers**: Must be in international format `+[country][number]`
- **Email**: Must be valid email format
- **Amount**: 0.00 to 10,000.00 GBP

## Testing

Run all tests:
```bash
dotnet test
```

Run specific test project:
```bash
dotnet test EagleBank.Tests
dotnet test EagleBank.Repository.Tests
dotnet test EagleBank.Orchestration.Tests
```

## Known Limitations

1. **In-Memory Storage**: Data is lost when application restarts
2. **No Authentication Endpoint**: JWT tokens must be generated manually
3. **Missing Business Rules**: 
   - No balance validation for withdrawals
   - No user-account relationship validation
4. **Configuration**: JWT settings need manual configuration

## Error Responses

The API returns structured error responses:

```json
{
  "message": "Error description"
}
```

For validation errors:
```json
{
  "message": "The request didn't supply all the necessary data",
  "details": [
    {
      "field": "fieldName",
      "message": "Field is required",
      "type": "validation"
    }
  ]
}
```

## HTTP Status Codes

- `200` - Success
- `201` - Created
- `204` - No Content (successful deletion)
- `400` - Bad Request (validation errors)
- `401` - Unauthorized (missing/invalid token)
- `403` - Forbidden (insufficient permissions)
- `404` - Not Found
- `422` - Unprocessable Entity (business rule violation)
- `500` - Internal Server Error

## Development

### Project Structure
```
EagleBank/
├── EagleBank/                     # API layer
├── EagleBank.Models/              # Shared models and DTOs
├── EagleBank.Orchestration/       # Business logic
├── EagleBank.Repository/          # Data access
├── EagleBank.Tests/               # API tests
├── EagleBank.Orchestration.Tests/ # Business logic tests
└── EagleBank.Repository.Tests/    # Repository tests
```

### Adding New Features

1. Add models to `EagleBank.Models`
2. Create repository interface and implementation
3. Add orchestrator for business logic
4. Create controller endpoints
5. Add comprehensive unit tests

## Contributing

1. Follow existing code patterns
2. Add unit tests for new features
3. Update this README for significant changes
4. Ensure all tests pass before submitting

## License

This project is for educational/demonstration purposes.