---
config:
  theme: neo-dark
---
sequenceDiagram
    autonumber
    participant C as Client
    participant Ctrl as OrderController
    participant S as OrderService
    participant P as PaymentService
    participant R as OrderRepository

    C->>Ctrl: POST /orders
    activate Ctrl

    Ctrl->>S: placeOrder(dto)
    activate S

    S->>P: process(paymentDetails)
    activate P

    P-->>S: payment ok
    deactivate P

    S->>R: save(order)
    activate R

    R-->>S: saved
    deactivate R

    S-->>Ctrl: orderId
    deactivate S
    Ctrl-->>C: 201 Created
    deactivate Ctrl