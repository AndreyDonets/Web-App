using FluentValidation;
using PL.Models;
using System;

namespace PL.Validation
{
    public class OrderValidator : AbstractValidator<OrderViewModel>
    {
		public OrderValidator()
		{
			RuleFor(x => x.FirstName).NotEmpty().Length(1, 50);
			RuleFor(x => x.LastName).NotEmpty().Length(1, 50);
			RuleFor(x => x.Weight).GreaterThan(0);
			RuleFor(x => x.CityId).NotEqual(new Guid());
			RuleFor(x => x.SendType).IsInEnum();
		}
	}
}
