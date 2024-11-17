using FluentValidation;
using OrdersService.RequestsResponsesModels.RequestModels;

namespace OrdersService.Validators;

public class CreateOrderRequestModelValidator : AbstractValidator<CreateOrderRequestModel>
{
    public CreateOrderRequestModelValidator()
    {
        RuleFor(model => model.OrderId)
            .NotEmpty()
            .WithMessage("Order ID is required.");

        RuleFor(model => model.ProductId)
            .NotEmpty()
            .WithMessage("Product ID is required.");

        RuleFor(model => model.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than zero.");
    }
}
