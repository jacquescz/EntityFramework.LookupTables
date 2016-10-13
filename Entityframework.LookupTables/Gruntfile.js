/// <binding Clean='clean' />
/*global module */
module.exports = function (grunt) {
    'use strict';

    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),
        clean: {
            nupkg: [".nupkg/*.nupkg"]
        },
    });

    grunt.registerTask('nugetpack', 'Create a nuget package', function () {
        var done = this.async();
        var csproj = grunt.file.expand({ filter: "isFile", cwd: "./" }, ["*.csproj"]);

        grunt.util.spawn({
            cmd: "nuget.exe",
            args: [
                "pack",
                csproj[0],
                "-OutputDirectory",
                ".nupkg",
            ]
        }, function (error, result) {
            if (error) {
                grunt.log.error(error);
            } else {
                grunt.log.write(result);
            }
            done();
        });
    });

    grunt.registerTask('nugetpush', 'Publish a nuget package', function () {
        var done = this.async();
        var nupkg = grunt.file.expand({ filter: "isFile", cwd: ".nupkg" }, ["*.nupkg"]);

        grunt.util.spawn({
            cmd: "nuget.exe",
            args: [
                "push",
                ".nupkg/" + nupkg[0],
                process.env.NUGETKEY,
                "-Source",
                "https://www.nuget.org/api/v2/package",

            ]
        }, function (error, result) {
            if (error) {
                grunt.log.error(error);
            } else {
                grunt.log.write(result);
            }
            done();
        });
    });
    grunt.loadNpmTasks('grunt-contrib-clean');

    grunt.registerTask('publish', ['clean', 'nugetpack', 'nugetpush']);
};