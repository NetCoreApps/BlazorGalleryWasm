export default {
    content: [
        './Components/**/*.{razor,html,cshtml}',
        '../MyApp.Client/**/*.{razor,html,cshtml}',
        './wwwroot/tailwind/**/*.html',
    ],
    theme: {
        extend: {
            colors: {
                'accent-1': '#FAFAFA',
                'accent-2': '#EAEAEA',
                danger: 'rgb(153 27 27)',
                success: 'rgb(22 101 52)',
            },
        },
    }
}