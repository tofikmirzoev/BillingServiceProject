using AutoMapper;
using BillingAPI.BillingMessages;
using BillingAPI.BillingResponses;
using BillingAPI.Constants;
using BillingAPI.DTO;
using BillingAPI.Models;
using BillingAPI.Repository.UnitOfWork;
using BillingAPI.ServiceIntefaces;
using FluentResults;

namespace BillingAPI.Services;

public class DepositService : IDepositService
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public DepositService(UnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public Result<ICollection<DepositDTO>> GetAllDeposits()
    {
        return Result.Ok(_mapper.Map<ICollection<DepositDTO>>(_unitOfWork.Deposits.GetAllDeposits()));
    }

    public Result<ICollection<DepositDTO>> GetDeposits(string accountId)
    {
        return Result.Ok(_mapper.Map<ICollection<DepositDTO>>(_unitOfWork.Deposits.GetDeposits(accountId)));
    }

    public Result<DepositDTO> GetDepositDetails(string depositId)
    {
        return Result.Ok(_mapper.Map<DepositDTO>(_unitOfWork.Deposits.GetDepositDetails(depositId)));
    }

    public Result RegisterDeposit(RegisterDepositRequest registerDepositRequest)
    {
        if (registerDepositRequest.CloseDate == null ||
            registerDepositRequest.InterestRate == null ||
            registerDepositRequest.DepositStatus == null ||
            registerDepositRequest.DepositTerm == null ||
            registerDepositRequest.InterestRate == null ||
            registerDepositRequest.OpenDate == null ||
            registerDepositRequest.DepositAmount == null ||
            registerDepositRequest.OpenDate == null
           )
            return Result.Fail("Null value");

        var transactionCommit = CreateTransactionRequest();
        var registrationResult = _unitOfWork.Deposits.RegisterDeposit(registerDepositRequest);
        if (!registrationResult)
            return Result.Fail("Some error happened while registration");
        
        return Result.Ok();
    }

    public Result<UpdateDepositResponse> UpdateDeposits(UpdateDepositsRequest request)
    {
        var deposits = new List<Deposits>();
        foreach (var depositDto in request.DepositDtos)
        {
            var depositToUpdate = _unitOfWork.Deposits.GetDepositDetails(depositDto.DepositId);

            depositToUpdate.DepositBalance = depositDto.DepositBalance;
            depositToUpdate.InitialDepositBalance = depositDto.InitialDepositBalance;
            depositToUpdate.DepositTerm = depositDto.DepositTerm;
            depositToUpdate.DepositStatus = depositDto.DepositStatus;

            deposits.Add(depositToUpdate);
        }

        if (!_unitOfWork.Deposits.UpdateDeposits(deposits))
        {
            var failedResponse = new UpdateDepositResponse()
            {
                resultCode = RequestResultCodes.UnexptectedError,
                resultDescription = RequestResultDescription.UnexptectedError
            };
            return Result.Ok(failedResponse);
        }
        
        var successfulResponse = new UpdateDepositResponse()
        {
            resultCode = RequestResultCodes.Successful,
            resultDescription = RequestResultDescription.Successful
        };
        
        return Result.Ok(successfulResponse);
    }

    public Result CloseDeposit(string depositId)
    {
        if (depositId == null)
            return Result.Fail("Null value");

        var deposit = _unitOfWork.Deposits.GetDepositDetails(depositId);
        if (deposit == null)
            return Result.Fail("Not Found");

        if (deposit.DepositStatus == "Closed")
            return Result.Fail("Deposit already closed");

        var depositClosingResult = _unitOfWork.Deposits.CloseDeposit(depositId);
        if (!depositClosingResult)
            return Result.Fail("Something went wrong while saving");

        return Result.Ok();

    }

    public PaymentRequest CreateTransactionRequest()
    {
        throw new NotImplementedException();
    }
}