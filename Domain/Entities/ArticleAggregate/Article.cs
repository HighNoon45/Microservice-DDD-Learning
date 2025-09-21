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
        public string Nr {  get; private set; }
        public string Name { get; private set; }

        public Article(string nr, string name)
        {
            Nr = Guard.Against.NullOrEmpty(nr);
            Name = Guard.Against.NullOrEmpty(name);
            Nr = nr;
            Name = name;
        }
    }
}
