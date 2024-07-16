<script>
import Button from '../components/Button.vue';

export default {
    name: "DecksPage",
    components: {
        Button
    },

    methods: {

    },
    
    data() {
        return {
            fractionName: "Fraction",
            fractionImage: "assets/images/registration/fraction.png"
        }
    }
}
</script>

<template>
    <section class="h-[calc(100vh-90px)] w-screen justify-start bg-cover bg-no-repeat bg-[url('./assets/images/registration/background.png')] text-[#ffffff]">
        <header>
            <!-- FRACTIONS -->
            <div class="flex justify-center">
                <button> <- </button>
                <h1 class="flex mx-10">
                    <img src="../assets/images/registration/logo.png" class="w-[80px] mr-2" />
                    {{ fractionName }}
                </h1>
                <button> -> </button>
            </div>
            <!-- FRACTIONS -->
        </header>

        <div class="flex gap-5 w-screen justify-center">
            <!-- COLLECTION CARDS -->
            <section class="w-[20vw]">
                <div class="">Collection cards</div>
                <div class="grid grid-rows-3 grid-flow-col gap-4 bg-[#151515]">
                    <!-- <Card /> -->
                </div>
            </section>
            <!-- COLLECTION CARDS -->
            
            <!-- LIDER -->
            <main class="w-[15vw]">
                <div class="text-center text-[#F1D177]">Lider</div>
                <img :src="fractionImage" class="w-full" />
            </main>
            <!-- LIDER -->

            <!-- DECK CARDS -->
            <section class="w-[20vw]">
                <div class="text-end">Deck cards</div>
                <div class="grid grid-rows-3 grid-flow-col gap-4 bg-[#151515]">
                    <!-- <Card /> -->
                </div>
            </section>
            <!-- DECK CARDS -->
        </div>
    </section>
</template>

<style scoped>
* {
    font-family: "Witcher";
}

.button-custom {
    width: 280px;
    height: 80px;
}
</style>