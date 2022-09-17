using AuthService.Entities;

namespace AuthService.Utils.Mappers;
public static class OptCodeMappers
{
  public static OptCodeModel CreateBasicModel(this OptCodeModel codeModel,
                                                  long userId, string code)
  {
    codeModel.UserId = userId;
    codeModel.Code = code;
    return codeModel;
  }
  public static OptCodeModel CreateBasicModelWithRandomCode(this OptCodeModel codeModel,long userId)
  {
    string code = new Random().Next(4).ToString();
    codeModel.UserId = userId;
    codeModel.Code = code;
    return codeModel;
  }
}

