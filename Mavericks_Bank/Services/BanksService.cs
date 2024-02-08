﻿using Mavericks_Bank.Exceptions;
using Mavericks_Bank.Interfaces;
using Mavericks_Bank.Models;
using Mavericks_Bank.Models.DTOs;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Mavericks_Bank.Services
{
    public class BanksService : IBanksAdminService
    {
        private readonly IRepository<int,Banks> _banksRepository;
        private readonly ILogger<BanksService> _loggerBanksService;

        public BanksService(IRepository<int, Banks> banksRepository, ILogger<BanksService> loggerBanksService)
        {
            _banksRepository = banksRepository;
            _loggerBanksService = loggerBanksService;
        }

        public async Task<Banks> AddBank(Banks bank)
        {
            var allBanks = await _banksRepository.GetAll();
            if(allBanks != null)
            {
                if(allBanks.Contains(bank))
                {
                    throw new BankNameAlreadyExistsException("Bank Name Already Exists");
                }
            }
            return await _banksRepository.Add(bank);
        }

        public async Task<Banks> DeleteBank(int bankID)
        {
            var deletedBank = await _banksRepository.Delete(bankID);
            if(deletedBank == null)
            {
                throw new NoBanksFoundException($"Bank ID {bankID} not found");
            }
            return deletedBank;
        }

        public async Task<List<Banks>> GetAllBanks()
        {
            var allBanks = await _banksRepository.GetAll();
            if(allBanks == null)
            {
                throw new NoBanksFoundException($"No Banks Data Found");
            }
            return allBanks;
        }

        public async Task<Banks> GetBank(int bankID)
        {
            var foundedBank = await _banksRepository.Get(bankID);
            if(foundedBank == null)
            {
                throw new NoBanksFoundException($"Bank ID {bankID} not found");
            }
            return foundedBank;
        }

        public async Task<Banks> UpdateBankName(UpdateBankNameDTO updateBankNameDTO)
        {
            var foundedBank = await GetBank(updateBankNameDTO.BankID);
            foundedBank.BankName = updateBankNameDTO.BankName;
            var updatedBank = await _banksRepository.Update(foundedBank);
            return updatedBank;
        }
    }
}
