using Application.Dtos;

namespace Domain.Interfaces;

public interface IUserState
{
    CurrentUser GetCurrentUser();
}