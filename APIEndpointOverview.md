## API Endpoint Overview

| Method | Endpoint                             | Role Required | Input Required                             | Description |
|--------|--------------------------------------|---------------|---------------------------------------------|-------------|
| GET    | `/api/products/status`               | Anonymous     | None                                        | Health check for the Product API |
| GET    | `/api/products`                      | User/Admin    | None                                        | Retrieves all products |
| GET    | `/api/products/{id}`                 | User/Admin    | `id` (path)                                 | Retrieves a product by ID |
| GET    | `/api/products/search?prefix=abc`    | User/Admin    | `prefix` (query)                            | Retrieves products whose name starts with the prefix |
| POST   | `/api/auth/login`                    | Anonymous     | `LoginRequest` JSON `{ username, password }`| Authenticates user and returns JWT token |
| POST   | `/api/user/adduser`                  | Anonymous     | `User` JSON                                 | Registers a new user |
| POST   | `/api/user/addtocart`                | User          | `userId`, `IdProduct`, `quantity` (query)   | Adds a product to a user’s cart |
| POST   | `/api/order/addorder`                | User          | `Order` JSON                                | Submits an order directly |
| POST   | `/api/order/ordercart?userId={id}`   | User          | `userId` (query)                            | Submits the current cart as an order |
| GET    | `/api/admin/test`                    | Admin         | None                                        | Confirms AdminController is working |
| GET    | `/api/admin/users`                   | Admin         | None                                        | Lists all registered users |
| GET    | `/api/admin/products`                | Admin         | None                                        | Lists all products (admin view) |
| POST   | `/api/admin/product`                 | Admin         | `Product` JSON                              | Adds a new product |
| PUT    | `/api/admin/product`                 | Admin         | `Product` JSON                              | Edits an existing product |
| DELETE | `/api/admin/product/{id}`            | Admin         | `id` (path)                                 | Deletes a product by ID |
| GET    | `/api/admin/top-products`            | Admin         | None                                        | Returns the top-selling products |
| GET    | `/api/admin/monthly-sales`           | Admin         | None                                        | Returns monthly sales report |

All endpoints that require a User or Admin role must include a valid JWT token in the Authorization: Bearer <token> header.

For Admin-only endpoints, the token must include the role: Admin claim.

Body inputs must be sent as application/json.
