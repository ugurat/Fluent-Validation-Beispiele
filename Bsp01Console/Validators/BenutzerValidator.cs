using Bsp01.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bsp01.Validators
{
    internal class BenutzerValidator : AbstractValidator<Benutzer>
    {

        public BenutzerValidator()
        {

            // Festlegung der Regeln

            RuleFor(b => b.Name) // Zugriff auf die Eigenschaften aus der Klasse "Benutzer"
            .NotEmpty().WithMessage("Der Name darf nicht leer sein.")
            .Length(2, 50).WithMessage("Der Name muss zwischen 2 und 50 Zeichen lang sein.");

            RuleFor(b => b.Alter) // Zugriff auf die Eigenschaften aus der Klasse "Benutzer"
                .GreaterThan(0).WithMessage("Das Alter muss größer als 0 sein.")
                .LessThanOrEqualTo(120).WithMessage("Das Alter darf höchstens 120 Jahre betragen.");

        }



    }
}
