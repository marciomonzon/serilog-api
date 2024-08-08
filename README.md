# ASP.NET API with Serilog

![Design sem nome](https://github.com/user-attachments/assets/e3ba08f4-6213-490a-a8a3-43f674c32c82)


<p>
  In a compliance perspective of applications, it is important to log at least info and
  errors of your application running.
</p>
<p>
  To achieve that, there are many solutions and one of them is Serilog, a great .NET library which do the job with maestry.
</p>

<p>
  This is only an example project, you should not use it in a Production environment.
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

<br>

* File settings:
```ruby
Log.Logger = new LoggerConfiguration()
.MinimumLevel.Error()
.WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
.CreateLogger();
```

<br>

* Screenshot of the file:
<img width="507" alt="image" src="https://github.com/user-attachments/assets/b080c108-0ec6-44f5-acda-f1eab12d8fbf">

<br>

* Showing the Exception message in details. Take a look on "Invalid email format", one of the validations required.
<img width="698" alt="image" src="https://github.com/user-attachments/assets/b01052a5-a8b0-42b1-96dd-f52420166551">

<br>

## Curiosities
* You can write your logs into a Console, File or a Database Table;
* To write into a file, database or console, you need the corresponding sink package.

<br>

## Documentation
* Serilog: https://serilog.net/
* Sinks: https://github.com/serilog/serilog/wiki/Provided-Sinks
* SQL Server Sink: https://github.com/serilog-mssql/serilog-sinks-mssqlserver


