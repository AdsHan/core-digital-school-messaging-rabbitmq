﻿using DSC.Core.Commands;
using DSC.Student.API.Application.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;

namespace DSC.Student.API.Application.Messages.Commands.StudentCommand
{

    public class UpdateStudentCommand : Command
    {
        public Guid StudentId { get; set; }
        public string Name { get; set; }
        public DateTime DateBirth { get; set; }
        public string Rg { get; set; }
        public string Cpf { get; set; }
        public string Note { get; set; }
        public Guid CourseId { get; set; }
        public List<GuardianDTO> Guardians { get; set; }
        public AdressDTO Adress { get; set; }

        public override bool Validate()
        {
            BaseResult.ValidationResult = new UpdateStudentValidation().Validate(this);
            return BaseResult.ValidationResult.IsValid;
        }

        public class UpdateStudentValidation : AbstractValidator<UpdateStudentCommand>
        {
            public UpdateStudentValidation()
            {
                RuleFor(c => c.StudentId)
                    .NotEmpty()
                    .WithMessage("O código do aluno não foi informado");

                RuleFor(c => c.Name)
                    .NotEmpty()
                    .WithMessage("O nome do aluno não foi informado");

                RuleFor(c => c.Name)
                    .MaximumLength(200)
                    .WithMessage("Tamanho máximo do nome é de 200 caracteres.");

                RuleFor(c => c.Note)
                    .MaximumLength(8000)
                    .WithMessage("Tamanho máximo da observação é de 8000 caracteres.");

                RuleFor(c => c.Cpf)
                    .Must(TerCpfValido)
                    .WithMessage("O CPF informado não é válido.");

                RuleFor(c => c.Rg)
                    .Must(TerRgValido)
                    .WithMessage("O RG informado não é válido.");

                RuleFor(c => c.Guardians.Count)
                    .GreaterThan(0)
                    .WithMessage("O Aluno precisa ter no mínimo 1 responsável");
            }

            protected static bool TerCpfValido(string cpf)
            {
                return Core.DomainObjects.Cpf.Validate(cpf);
            }

            protected static bool TerRgValido(string Rg)
            {
                return Core.DomainObjects.Rg.Validate(Rg);
            }
        }
    }
}