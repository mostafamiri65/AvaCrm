using AvaCrm.Application.Models;

namespace AvaCrm.Application.Contracts;

public interface ICaptchaGenerator
{
    CaptchaChallenge GenerateChallenge();
    string GeneratorType { get; }
}
