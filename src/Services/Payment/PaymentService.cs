 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using src.Repository;
using src.Database;
using src.Entity;


using static src.DTO.PaymentDTO;

namespace src.Services.Payment
{
    public class PaymentService : IPaymentService
    {
        protected readonly PaymentRepository _paymentRepo;
        protected readonly IMapper _mapper;
        public PaymentService (PaymentRepository paymentRepo, IMapper mapper)
        {
            _paymentRepo = paymentRepo;
            _mapper = mapper;
        }

        public async Task <PaymentReadDto> CreateOneAsync(PaymentCreateDto createDto)
        {
            var payment = _mapper.Map<PaymentCreateDto, src.Entity.Payment>(createDto);
            var paymentCreated = await _paymentRepo.CreateOneAsync(payment);
            return _mapper.Map<src.Entity.Payment,PaymentReadDto>(paymentCreated);
        }

        public async Task<List<PaymentReadDto>> GetAllAsync()
        {
            
            var paymentList= await _paymentRepo.GetAllAsync();
            return _mapper.Map<List<src.Entity.Payment>, List<PaymentReadDto>>(paymentList);
        }



        public async Task<PaymentReadDto> GetByIdAsync(Guid paymentId)
        {
            var foundPayment = await _paymentRepo.GetByIdAsync(paymentId);
            return _mapper.Map<src.Entity.Payment, PaymentReadDto> (foundPayment);
        }

        public async Task<bool> DeleteOneAsync(Guid paymentId)
        {
            var foundPayment = await _paymentRepo.GetByIdAsync(paymentId);
           bool IsDeleted = await _paymentRepo.DeleteOneAsync(foundPayment);

           if(IsDeleted)
           {    
                return true;
           }
           return false;
        }

        public async Task<bool> UpdateOneAsync(Guid paymentId, PaymentUpdateDto updateDto)
        {
            var foundPayment = await _paymentRepo.GetByIdAsync(paymentId);
            var isUpdated = await _paymentRepo.UpdateOneAsync(foundPayment);

            if (foundPayment==null)
            {
                return false;
            }

            _mapper.Map(updateDto, foundPayment);
            return await _paymentRepo.UpdateOneAsync(foundPayment);
            
        }

        public Task<List<PaymentReadDto>> GetAllAsynac()
        {
            throw new NotImplementedException();
        }
        
        public Task<PaymentReadDto> GetByIdAsynac(Guid paymentId)
        {
            throw new NotImplementedException();
        }


    }
}

