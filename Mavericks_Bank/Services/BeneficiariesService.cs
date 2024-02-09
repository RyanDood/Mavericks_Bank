﻿using Mavericks_Bank.Exceptions;
using Mavericks_Bank.Interfaces;
using Mavericks_Bank.Models;

namespace Mavericks_Bank.Services
{
    public class BeneficiariesService : IBeneficiariesAdminService
    {
        private readonly IRepository<long,Beneficiaries> _beneficiariesRepository;
        private readonly ICustomersAdminService _customersService;
        private readonly ILogger<BeneficiariesService> _loggerBeneficiariesService;

        public BeneficiariesService(IRepository<long, Beneficiaries> beneficiariesRepository, ICustomersAdminService customersService, ILogger<BeneficiariesService> loggerBeneficiariesService)
        {
            _beneficiariesRepository = beneficiariesRepository;
            _customersService = customersService;
            _loggerBeneficiariesService = loggerBeneficiariesService;
        }

        public async Task<Beneficiaries> AddBeneficiary(Beneficiaries beneficiary)
        {
            var allBeneficiaries = await _beneficiariesRepository.GetAll();
            if(allBeneficiaries != null)
            {
                if (allBeneficiaries.Contains(beneficiary))
                {
                    throw new BeneficiaryAlreadyExistsException($"Beneficiary Account Number {beneficiary.AccountNumber} already exists");
                }
            }
            return await _beneficiariesRepository.Add(beneficiary);
        }

        public async Task<Beneficiaries> DeleteBeneficiary(long accountNumber)
        {
            var deletedBeneficiary = await _beneficiariesRepository.Delete(accountNumber);
            if(deletedBeneficiary == null)
            {
                throw new NoBeneficiariesFoundException($"Beneficiary Account Number {accountNumber} not found");
            }
            return deletedBeneficiary;
        }

        public async Task<List<Beneficiaries>> GetAllBeneficiaries()
        {
            var allBeneficiaries = await _beneficiariesRepository.GetAll();
            if(allBeneficiaries == null)
            {
                throw new NoBeneficiariesFoundException("No Available Beneficiaries Data");
            }
            return allBeneficiaries;
        }

        public async Task<List<Beneficiaries>> GetAllCustomerBeneficiaries(int customerID)
        {
            await _customersService.GetCustomer(customerID);
            var allBeneficiaries = await GetAllBeneficiaries();
            var allCustomerBeneficiaries = allBeneficiaries.Where(beneficiary => beneficiary.CustomerID ==  customerID).ToList();
            if( allCustomerBeneficiaries.Count == 0)
            {
                throw new NoBeneficiariesFoundException($"No Beneficiaries found for Customer ID {customerID}");
            }
            return allCustomerBeneficiaries;
        }

        public async Task<Beneficiaries> GetBeneficiary(long accountNumber)
        {
            var foundBeneficiary = await _beneficiariesRepository.Get(accountNumber);
            if(foundBeneficiary == null)
            {
                throw new NoBeneficiariesFoundException($"Beneficiary Account Number {accountNumber} not found");
            }
            return foundBeneficiary;
        }
    }
}