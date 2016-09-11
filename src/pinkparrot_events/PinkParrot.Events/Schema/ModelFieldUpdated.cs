﻿// ==========================================================================
//  ModelFieldUpdated.cs
//  PinkParrot Headless CMS
// ==========================================================================
//  Copyright (c) PinkParrot Group
//  All rights reserved.
// ==========================================================================

using PinkParrot.Core.Schema;
using PinkParrot.Infrastructure;

namespace PinkParrot.Events.Schema
{
    [TypeName("ModelFieldUpdatedEvent")]
    public class ModelFieldUpdated : TenantEvent
    {
        public long FieldId;

        public IModelFieldProperties Properties;
    }
}
