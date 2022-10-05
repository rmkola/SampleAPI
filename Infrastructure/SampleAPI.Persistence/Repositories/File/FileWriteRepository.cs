﻿using SampleAPI.Application.Repositories;
using SampleAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleAPI.Persistence.Repositories
{
    public class FileWriteRepository : WriteRepository<SampleAPI.Domain.Entities.File>, IFileWriteRepository
    {
        public FileWriteRepository(SampleAPIDbContext context) : base(context)
        {
        }
    }
}
