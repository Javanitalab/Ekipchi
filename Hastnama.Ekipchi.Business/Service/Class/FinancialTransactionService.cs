using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Hastnama.Ekipchi.Business.Service.Interface;
using Hastnama.Ekipchi.Common.Enum;
using Hastnama.Ekipchi.Common.Helper;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Common.Result;
using Hastnama.Ekipchi.Data.Financial;
using Hastnama.Ekipchi.DataAccess.Context;
using Hastnama.Ekipchi.DataAccess.Entities;
using Hastnama.Ekipchi.DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hastnama.Ekipchi.Business.Service.Class
{
    public class FinancialTransactionService : Repository<EkipchiDbContext, FinancialTransaction>,
        IFinancialTransactionService
    {
        private readonly IMapper _mapper;

        public FinancialTransactionService(EkipchiDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }


        public async Task<Result<PagedList<FinancialTransactionDto>>> List(
            FilterFinancialTransactionQueryDto filterQueryDto)
        {
            var transactions = await WhereAsyncAsNoTracking(c =>
                    (filterQueryDto.FromDate == null || c.CreateDate > filterQueryDto.FromDate)
                    && (filterQueryDto.ToDate == null || c.CreateDate < filterQueryDto.ToDate)
                    && (filterQueryDto.PayerUsername == null ||
                        c.Payer.Username.ToLower().Contains(filterQueryDto.PayerUsername.ToLower()))
                    && (filterQueryDto.ReceiverUsername == null || c.Receiver.Username.ToLower()
                            .Contains(filterQueryDto.ReceiverUsername.ToLower()))
                    && (filterQueryDto.AuthorUsername == null || c.Author == null || c.Author.Username.ToLower()
                            .Contains(filterQueryDto.AuthorUsername.ToLower()))
                , filterQueryDto, t => t.Payments, t => t.Payer, t => t.Author, t => t.Receiver);

            var response = transactions.MapTo<FinancialTransactionDto>(_mapper);
            return Result<PagedList<FinancialTransactionDto>>.SuccessFull(response);
        }

        public async Task<Result<FinancialTransactionDto>> Get(Guid id)
        {
            var transaction = await FirstOrDefaultAsyncAsNoTracking(c => c.Id == id, t => t.Payments, t => t.Payer,
                t => t.Author, t => t.Receiver);

            if (transaction == null)
                return Result<FinancialTransactionDto>.Failed(new NotFoundObjectResult(new ApiMessage
                    {Message = ResponseMessage.TransactionNotFound}));

            return Result<FinancialTransactionDto>.SuccessFull(
                _mapper.Map<FinancialTransactionDto>(transaction));
        }
    }
}