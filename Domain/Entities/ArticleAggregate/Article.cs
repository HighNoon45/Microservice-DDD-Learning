using Ardalis.GuardClauses;
using Domain.Interfaces;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ArticleAggregate
{
    public class Article : BaseEntity, IAggregateRoot
    {
        private Article() { }
        public string Nr { get; private set; } = string.Empty;
        public string Name { get; private set; } = string.Empty;

        public Article(string nr, string name)
        {
            Nr = Guard.Against.NullOrEmpty(nr);
            Name = Guard.Against.NullOrEmpty(name);
            Nr = nr;
            Name = name;
        }
    }
}
