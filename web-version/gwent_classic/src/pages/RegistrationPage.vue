<script>
export default {
	data() {
		return {
			
		}
	}
}
</script>

<template>
	<section class="flex flex-col items-center h-screen w-screen bg-[#242424] bg-cover bg-no-repeat bg-[url('./assets/images/registration/background.svg')]">
		<div class="text-center text-white mt-20 mb-20">
			<h3 class="font-witcher-alternative text-9xl">Witcher</h3>
			<h1 class="font-witcher-alternative mt-6 text-6xl">Gwent classic</h1>
		</div>
		<form class="mt-10 p-8 w-[500px] bg-[#6a752d] bg-opacity-80 rounded-lg shadow-lg">
			<div class="flex flex-col space-y-6">
				<input class="p-4 text-xl rounded-md bg-[#2c2c2c] text-white placeholder-[#9e9a9a]" type="text" placeholder="LOGIN"/>
				<input class="p-4 text-xl rounded-md bg-[#2c2c2c] text-white placeholder-[#9e9a9a]" type="password" placeholder="PASSWORD"/>
				<input class="p-4 text-xl rounded-md bg-[#2c2c2c] text-white placeholder-[#9e9a9a]" type="email" placeholder="EMAIL"/>
				<button class="mt-6 py-4 bg-[#a73535] text-white text-xl rounded-md hover:bg-[#b22222] focus:outline-none focus:ring-2 focus:ring-[#8b0000]" type="submit">Register</button>
			</div>
		</form>
	</section>
</template>

<style scoped>
* :not(h1, h3) {
	font-family: "Witcher";
}
</style>