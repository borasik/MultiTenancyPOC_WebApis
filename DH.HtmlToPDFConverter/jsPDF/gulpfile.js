"use strict";
var gulp = require("gulp");
var zip = require('gulp-zip');
var del = require("del");
var sourcemaps = require('gulp-sourcemaps');
var path = require('path');
var minimist = require('minimist');
var typescript = require('gulp-typescript');
var tscConfig = require('./tsconfig.json');
var fs = require('fs');

var knownOptions = {
    string: 'packageName',
    string: 'packagePath',
    default: { packageName: "Package.zip", packagePath: path.join(__dirname, '_package') }
}

var options = minimist(process.argv.slice(2), knownOptions);

/**
 * Remove build directory.
 */
gulp.task('clean', function (cb) {
    console.log("Cleaning and removing build and package folder ...");

    del("_package", cb);
    return del(["build"], cb);
});

// TypeScript compile
gulp.task('compile', ['clean'], function () {
    console.log("Transpiling typescript files ...");

    return gulp
      .src('app/**/*.ts')
      .pipe(typescript(tscConfig.compilerOptions))
      .pipe(gulp.dest('build/app/transpiled-files'));
});

/**
 * Copy all resources that are not TypeScript files into build directory.
 */
gulp.task("resources", ["app", "images"], function () {
    console.log("Building resources...");
});

/* copy the app core files to the build folder */
gulp.task("app", ['index'], function () {
    return gulp.src(["app/**", "!app/**/*.ts"])
        .pipe(gulp.dest("build/app"));
});

/* get the index file to the root of the build */
gulp.task("index", function () {
    return gulp.src(["index.html", "systemjs.config.js"])
        .pipe(gulp.dest("build"));
});

/* images */
gulp.task("images", function () {
    return gulp.src(["images/**"])
        .pipe(gulp.dest("build/images"));
});

/**
 * Copy all required libraries into build directory.
 */
gulp.task("libs", function () {
    return gulp.src(["node_modules/**"])
        .pipe(gulp.dest("build/node_modules"));
});

/**
 * Build the project.
 */
gulp.task("default", ['compile', 'resources', 'libs'], function () {
    console.log("Building the project ...");

    var packagePaths = ['build/**']

    //add exclusion patterns for all dev dependencies
    var packageJSON = JSON.parse(fs.readFileSync(path.join(__dirname, 'package.json'), 'utf8'));
    var devDeps = packageJSON.devDependencies;

    for (var propName in devDeps) {
        var excludePattern1 = "!**/node_modules/" + propName + "/**";
        var excludePattern2 = "!**/node_modules/" + propName;
        packagePaths.push(excludePattern1);
        packagePaths.push(excludePattern2);
    }

    return gulp.src(packagePaths)
        .pipe(zip(options.packageName))
        .pipe(gulp.dest(options.packagePath));
});
