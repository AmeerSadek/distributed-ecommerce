# Distributed E-commerce System
A simplified e-commerce system demonstrating inter-service communication using .NET Core 8, MassTransit, and RabbitMQ as a message broker. The project showcases a service-oriented architecture with Orders, Inventory, and Notification services, emphasizing asynchronous communication, event-driven workflows, and basic fault tolerance.

--- 

## Services Overview

### 1. **Orders Service**
- **Purpose**: Manages order creation and validation.
- **Functionality**:
  - Receives order requests containing Order ID, Product ID, and Quantity.
  - Validates the order.
  - Publishes `OrderCreated` events to RabbitMQ.

### 2. **Inventory Service**
- **Purpose**: Manages product inventory based on incoming orders.
- **Functionality**:
  - Subscribes to `OrderCreated` events.
  - Checks stock availability:
    - **In Stock**: Reduces inventory and publishes an `InventoryUpdated` event.
    - **Out of Stock**: Publishes an `OutOfStock` event.

### 3. **Notifications Service**
- **Purpose**: Notifies users about the order's status.
- **Functionality**:
  - Subscribes to `InventoryUpdated` and `OutOfStock` events.
  - Sends mock notifications (e.g., logs messages) to indicate:
    - If the order is processed successfully.
    - If the order is waiting due to low stock.

---

## System Architecture and Message Flow

The system uses an **event-driven architecture** with RabbitMQ to facilitate communication between microservices. Below is the message flow:
1. A new order is created in the **Orders Service**, triggering an `OrderCreated` event.
2. The **Inventory Service** processes the `OrderCreated` event:
   - Updates stock.
   - Publishes either an `InventoryUpdated` or `OutOfStock` event based on availability.
3. The **Notifications Service** processes these events:
   - Logs a success message for `InventoryUpdated`.
   - Logs a waiting notification for `OutOfStock`.

---

## Setup Instructions

### Prerequisites
Ensure the following are installed on your machine:
- [.NET Core 8+ SDK](https://dotnet.microsoft.com/)
- [Docker](https://www.docker.com/)

### Running RabbitMQ
A `docker-compose.yml` file is included to set up RabbitMQ. To start RabbitMQ, follow these steps:
1. Navigate to the root directory of the project where the `docker-compose.yml` file is located.
2. Run the following command:
   ```bash
   docker-compose up -d


## Key Design Choices

1. **Event-Driven Communication**
   - RabbitMQ was chosen as the message broker to ensure reliable asynchronous communication between services.
   - Events such as `OrderCreated`, `InventoryUpdated`, and `OutOfStock` drive the workflow across microservices.

2. **MassTransit Integration**
   - MassTransit simplifies RabbitMQ integration, providing a robust abstraction layer for:
     - Publishing messages.
     - Subscribing to messages.
     - Implementing error handling mechanisms.

3. **Fault Tolerance**
   - Retries are implemented for handling message processing failures, ensuring reliable delivery.
  
---

## Testing the System

Follow these steps to test the system and verify its functionality:

1. **Submit an Order**
   - Use a REST client like Postman, cURL, or any other tool to send a request to the Orders Service.
   - Provide the necessary order details, including `Order ID`, `Product ID`, and `Quantity`.

2. **Verify Inventory Service Behavior**
   - Check the Inventory Service logs to confirm that it processes the `OrderCreated` event.
   - The Inventory Service should:
     - Reduce stock if the product is in stock and publish an `InventoryUpdated` event.
     - Publish an `OutOfStock` event if the product is unavailable.

3. **Check Notifications**
   - Review the logs of the Notifications Service to verify that it correctly processes the events:
     - For an `InventoryUpdated` event, it should log a success notification.
     - For an `OutOfStock` event, it should log a notification indicating the order is waiting due to low stock.

4. **End-to-End Verification**
   - Confirm that the entire workflow (from order creation to notifications) functions as expected without errors.

---

### Notes
- Ensure all services (Orders, Inventory, and Notifications) are running before submitting an order.
- RabbitMQ should be running and accessible to all microservices during the test.
- In the `OrderCreated` consumer of the Orders Service, a retry mechanism is intentionally demonstrated by simulating failures during the first two processing attempts. This approach showcases how the system handles retries and ensures resilience when faced with transient errors, such as a `DatabaseServerDownException`.
