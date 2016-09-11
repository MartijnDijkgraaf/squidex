// ==========================================================================
//  ModelField.cs
//  PinkParrot Headless CMS
// ==========================================================================
//  Copyright (c) PinkParrot Group
//  All rights reserved.
// ==========================================================================

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PinkParrot.Infrastructure;
using PinkParrot.Infrastructure.Tasks;

// ReSharper disable InvertIf
// ReSharper disable ConvertIfStatementToReturnStatement

namespace PinkParrot.Core.Schema
{
    public abstract class ModelField
    {
        private readonly long id;
        private string name;
        private bool isDisabled;
        private bool isHidden;

        public long Id
        {
            get { return id; }
        }

        public string Name
        {
            get { return name; }
        }

        public bool IsHidden
        {
            get { return isHidden; }
        }

        public bool IsDisabled
        {
            get { return isDisabled; }
        }

        public abstract IModelFieldProperties RawProperties { get; }
        
        public abstract string Label { get; }

        public abstract string Hints { get; }

        public abstract bool IsRequired { get; }

        protected ModelField(long id, string name)
        {
            Guard.GreaterThan(id, 0, nameof(id));
            Guard.ValidSlug(name, nameof(name));

            this.id = id;

            this.name = name;
        }

        public abstract ModelField Update(IModelFieldProperties newProperties);

        public Task ValidateAsync(PropertyValue property, ICollection<string> errors)
        {
            Guard.NotNull(property, nameof(property));
            
            if (IsRequired && property.RawValue == null)
            {
                errors.Add("Field is required");
            }

            if (property.RawValue == null)
            {
                return TaskHelper.Done;
            }

            return ValidateCoreAsync(property, errors);
        }

        protected virtual Task ValidateCoreAsync(PropertyValue property, ICollection<string> errors)
        {
            return TaskHelper.Done;
        }

        public ModelField Hide()
        {
            return Update<ModelField>(clone => clone.isHidden = true);
        }

        public ModelField Show()
        {
            return Update<ModelField>(clone => clone.isHidden = false);
        }

        public ModelField Disable()
        {
            return Update<ModelField>(clone => clone.isDisabled = true);
        }

        public ModelField Enable()
        {
            return Update<ModelField>(clone => clone.isDisabled = false);
        }

        public ModelField Rename(string newName)
        {
            if (!newName.IsSlug())
            {
                var error = new ValidationError("Name must be a valid slug", "Name");

                throw new ValidationException($"Cannot rename the field '{name}' ({id})", error);
            }

            return Update<ModelField>(clone => clone.name = newName);
        }

        protected T Update<T>(Action<T> updater) where T : ModelField
        {
            var clone = (T)Clone();

            updater(clone);

            return clone;
        }

        protected abstract ModelField Clone();
    }
}