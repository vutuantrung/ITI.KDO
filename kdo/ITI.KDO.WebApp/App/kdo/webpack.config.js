var path = require('path');
var webpack = require('webpack');
var ExtractTextPlugin = require("extract-text-webpack-plugin");
require("babel-polyfill");
var wwwroot = "../../wwwroot";

module.exports = {
    entry: [ 'babel-polyfill' ,'./src/main.js'],

    output: {
        path: path.resolve(wwwroot, './dist'),
        publicPath: 'http://localhost:8080/dist/',
        filename: 'kdo.js'
    },

    module: {
        loaders: [{
                test: /\.vue$/,
                loader: 'vue-loader',
                options: {
                    loaders: {
                        css: ExtractTextPlugin.extract("css-loader"),
                        less: ExtractTextPlugin.extract("css-loader!less-loader")
                    }
                }
            },
            {
                test: /\.css$/,
                use: [
                { loader: "style-loader" },
                { loader: "css-loader" }
                ]
            },
            {
                test: /\.js$/,
                loader: 'babel-loader',
                exclude: /node_modules/
            },
            {
                test: /\.(png|jpg|gif|svg)$/,
                loader: 'file-loader',
                query: {
                    name: '[name].[ext]?[hash]'
                }
            },
            {
                test: /\.(woff2?|eot|ttf|otf)(\?.*)?$/,
                loader: 'file-loader',
                query: {
                    name: '[name].[ext]?[hash]'
                }
            }
        ]
    },

    devServer: {
        historyApiFallback: true,
        noInfo: true
    },

    watch: true,

    devtool: process.env.NODE_ENV === 'production' ? '#source-map' : '#eval-source-map',

    plugins: [
        new webpack.DefinePlugin({
            'process.env.NODE_ENV': JSON.stringify(process.env.NODE_ENV || 'development')
        }),

        // Bootstrap's Javascript is not module compliant, so we need to define the usual global variables of JQuery
        new webpack.ProvidePlugin({
            jQuery: 'jquery',
            $: 'jquery',
            jquery: 'jquery'
        }),

        new ExtractTextPlugin("style.css")
    ]
}

if (process.env.NODE_ENV === 'production') {
    module.exports.plugins = (module.exports.plugins || []).concat([
        new webpack.optimize.UglifyJsPlugin({
            compress: {
                warnings: false
            }
        })
    ])
}