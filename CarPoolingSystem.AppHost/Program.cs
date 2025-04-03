var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.CarPoolingSystem_Athu>("carpoolingsystem-athu");

builder.Build().Run();
