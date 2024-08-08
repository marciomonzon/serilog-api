# ASP.NET API with Serilog
<p>
  In a compliance perspective of applications, it is important to log at least info and
  errors of your application running.
</p>
<p>
  To achieve that, there are many solutions and one of them is Serilog, a great .NET library which do with maestry the job.
</p>

<br>

## Stack used
* ASP.NET Core;
* .NET 8 with C#;
* EntityFramework Core;
* Serilog.

<br>

## Nugget Packages
* Install-Package Serilog
* Install-Package Serilog.AspNetCore
* Install-Package Serilog.Sinks.File
* Install-Package Serilog.Sinks.Console
* Install-Package Serilog.Extensions.Logging
  
<br>

## Example explained
<p>
  This is an ASP.NET API which has the following characteristics:
</p>
* UserController with a POST endpoint;
* User Model;
* User Data Repository with an Interface;
* User Service with an Interface;
* Business Validations.

<p>
  When a business validation is not achieved, the app will throw an exception and this will be logged.
  The logs are being saved in a physical file.
</p>

<br>

* Method with Validations:
```ruby
private void HandleValidations(User user)
{
    if (string.IsNullOrWhiteSpace(user.Name))
        throw new ArgumentException("Name is required.");

    if (string.IsNullOrWhiteSpace(user.Email))
        throw new ArgumentException("Email is required.");

    if (!IsValidEmail(user.Email))
        throw new ArgumentException("Invalid email format.");
}
```

<br>

* Controller logging with Serilog:
```ruby
[HttpPost("add-user")]
public async Task<IActionResult> PostUser(User user)
{
    try
    {
        _logger.LogInformation("POST request received at {Time}", DateTime.UtcNow);

        var result = await _userService.AddUserAsync(user);

        return result ? Ok("User added successfully.")
               : BadRequest("User was not added");
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "An error occurred while processing the request.");
        return StatusCode(500, "Internal server error");
    }
    finally
    {
        _logger.LogInformation("POST request processing completed at {Time}", DateTime.UtcNow);
    }
}
```
