/** @type {import('tailwindcss').Config} */
export default {
  content: [
    "./index.html",
    "./src/**/*.{vue,js,ts,jsx,tsx}",
  ],
  theme: {
    extend: {},
    fontFamily: {
      'witcher': ['Witcher', 'sans-serif'],
      'witcher-alternative': ['Witcher Alternative', 'sans-serif'],
    }
  },
  plugins: [],
}

