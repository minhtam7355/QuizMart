using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuizMart.Context;
using QuizMart.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizMart.Repositories
{
    public class ChoiceRepository : IChoiceRepository
    {
        private readonly QuizMartDbContext _context;
        private readonly IMapper _mapper;

        public ChoiceRepository(QuizMartDbContext dbContext, IMapper mapper)
        {
            _context = dbContext;

            _mapper = mapper;

        }

        
    }
}
