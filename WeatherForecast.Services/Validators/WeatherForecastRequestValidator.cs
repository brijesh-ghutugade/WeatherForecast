using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecast.Contracts.Models;

namespace WeatherForecast.Services.Validators
{
    public class WeatherForecastRequestValidator : AbstractValidator<WeatherForecastRequest>
    {
        public WeatherForecastRequestValidator()
        {
            RuleFor(x => x.Latitude)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()                
                .WithMessage("Latitude required");

            RuleFor(x => x.Latitude)
               .Cascade(CascadeMode.Stop)
               .InclusiveBetween(-90,90)
               .WithMessage("Latitude must be in rage -90 to 90");

            RuleFor(x => x.Longitude)
               .Cascade(CascadeMode.Stop)
               .NotEmpty()
               .WithMessage("Longitude required");

            RuleFor(x => x.Longitude)
              .Cascade(CascadeMode.Stop)
              .InclusiveBetween(-180, 180)
              .WithMessage("Longitude must be in rage -180 to 180");
           

            RuleFor(x => x).Custom(CustomValidation);
        }

        private void CustomValidation(WeatherForecastRequest request, ValidationContext<WeatherForecastRequest> validationContext)
        {
            if (request.DailyVariables.Count > 0 && request.Timezone == null)
            {
                validationContext.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(request.Timezone), "Timezone is required if daily variables are supplies."));
                return;
            }

            if (request.ForecastDays > 0 && (request.StartDate != null || request.EndDate != null))
            {
                validationContext.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(request.ForecastDays), "Forecast days conflict with start date and end date."));
                return;
            }
            else if (request.ForecastDays <= 0 && (request.StartDate == null || request.EndDate == null))
            {
                validationContext.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(request.ForecastDays), "Starte and end date must be set."));
                return;
            }
        }
    }
}
