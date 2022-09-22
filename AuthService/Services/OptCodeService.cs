using AuthService.DataAccess.Repository.Interfaces;
using AuthService.Entities;
using AuthService.Interfaces;
using AuthService.Utils.Mappers;
using ErrorHandlingDll.ReturnTypes;
using GenericReositoryDll.Enumrations;
using GenericRepositoryDll.Repository.GenericRepository;
using System.Net;

namespace AuthService.Services;
public class OptCodeService : IOptCodeService
{
  private readonly IRepository<OptCodeModel> _codeRepository;
  private readonly IUserService _userService;
  public OptCodeService(IUnitOfWork unitOfWork , IUserService userService)
  {
    _codeRepository = unitOfWork.OptCodeRepository;
    _userService = userService;
  }
  public async Task<ReturnModel<long?>> CreateAsync(OptCodeModel codeModel)
  {
    ReturnModel<long?> result = new();
    await _codeRepository.AddAsync(codeModel);
    await _codeRepository.SaveAsync();

    result.CreateSuccessModel(data: codeModel.Id, "code Id");
    return result;
  }
  public async Task<ReturnModel<OptCodeModel>> GetAsync(long id)
  {
    ReturnModel<OptCodeModel> result = new();

    OptCodeModel code = await _codeRepository.FindAsync(id);
    if (code is null)
    {
      result.CreateNotFoundModel();
      return result;
    }

    result.CreateSuccessModel(data: code);
    return result;
  }
  public async Task<ReturnModel<OptCodeModel>> GetByCodeAsync(string code)
  {
    ReturnModel<OptCodeModel> result = new();

    OptCodeModel codeModel = await _codeRepository.GetSingleAsync(c=> c.Code == code);
    if (codeModel is null)
    {
      result.CreateNotFoundModel();
      return result;
    }

    result.CreateSuccessModel(data: codeModel);
    return result;
  }
  public async Task<ReturnModel<List<OptCodeModel>>> GetListAsync(short count = 10 , short offset = 0)
  {
    ReturnModel<List<OptCodeModel>> result = new();

    List<OptCodeModel> codes = await _codeRepository.GetListAsync<OptCodeModel>(skip: count, take: offset);

    result.CreateSuccessModel(data: codes);
    return result;
  }
  public async Task<ReturnModel<long?>> UpdateAsync(OptCodeModel codeModel)
  {
    ReturnModel<long?> result = new();

    await _codeRepository.UpdateAsync(codeModel);

    result.CreateSuccessModel(data: codeModel.Id, "Code Id");
    return result;
  }
  public async Task<string> CreateRandomCodeAsync(long userId)
  {
    OptCodeModel codeModel = new();
    codeModel.CreateBasicModelWithRandomCode(userId);

    var createOptCode = await CreateAsync(codeModel);
    if (createOptCode.HttpStatusCode is HttpStatusCode.OK && createOptCode.Data is not null)
      return codeModel.Code;

    return null;
  }
  public async Task<bool> ValidateOptCodeAsync(string optCode, long userId)
  {

    var getLastCode = await _userService.GetLastAuthCodeAsync(userId);
    string lastOptCode = getLastCode.Data.Code;

    if (getLastCode.HttpStatusCode == HttpStatusCode.NotFound
        || string.IsNullOrEmpty(lastOptCode)
        || string.IsNullOrWhiteSpace(lastOptCode)
        || lastOptCode != optCode)
      return false;

    return true;
  }
}

