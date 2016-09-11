// ==========================================================================
//  CreateModelSchema.cs
//  PinkParrot Headless CMS
// ==========================================================================
//  Copyright (c) PinkParrot Group
//  All rights reserved.
// ==========================================================================

using PinkParrot.Core.Schema;

namespace PinkParrot.Write.Schema.Commands
{
    public class CreateModelSchema : TenantCommand
    {
        public string Name { get; set; }

        public ModelSchemaProperties Properties { get; set; }
    }
}