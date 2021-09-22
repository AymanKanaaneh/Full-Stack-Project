using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using server_api_demos.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Newtonsoft;

namespace server_api_demos
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "server_api_demos", Version = "v1" });
            });

            //01) DECLARE INTERFACE PER SERVICES
            services.AddSingleton<IExamsRepository, SqlExamRepository>();

            //02) DECLARE INTERFACE PER SERVICES
            services.AddSingleton<ITeachersRepository, SqlTeacherRepository>();

            //03) DECLARE INTERFACE PER SERVICES
            services.AddSingleton<IQuestionsRepository, SqlQuestionsRepository>();

            //04) DECLARE INTERFACE PER SERVICES
            services.AddSingleton<IStudentsRepository, SqlStudentRepository>();

            //05) DECLARE INTERFACE PER SERVICES
            services.AddSingleton<IGradesRepository, SqlGradeRepository>();




        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "server_api_demos v1"));
            }

            //Enable Routing Of Static Files(HTML/CSS/JS)
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
