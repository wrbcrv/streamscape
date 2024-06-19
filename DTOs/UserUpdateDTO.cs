using System.ComponentModel.DataAnnotations;

public class UserUpdateDTO
{
    [Required(ErrorMessage = "Email é obrigatório")]
    [EmailAddress(ErrorMessage = "Email inválido")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Nome de usuário é obrigatório")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Senha atual é obrigatória para alteração de senha")]
    public string CurrentPassword { get; set; }

    [Required(ErrorMessage = "Senha é obrigatória.")]
    [StringLength(50, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 50 caracteres.")]
    public string NewPassword { get; set; }

    [Required(ErrorMessage = "Repetir senha é obrigatório.")]
    [Compare("NewPassword", ErrorMessage = "As senhas não coincidem.")]
    public string RepeatPassword { get; set; }
}
