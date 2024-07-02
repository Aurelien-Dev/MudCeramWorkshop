var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.MudCeramWorkshop_Client>("mudceramworkshop-client");

builder.Build().Run();
