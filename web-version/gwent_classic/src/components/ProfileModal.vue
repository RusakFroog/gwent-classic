<script>
import Button from './Button.vue'; 
import CustomInput from './CustomInput.vue';
import { logoutAccount, updateAccount } from '../services/accounts.js';

export default {
    name: "ProfileModal",
    components: {
        Button,
        CustomInput
    },

    data() {
        return {
            image: 'https://images-wixmp-ed30a86b8c4ca887773594c2.wixmp.com/f/ab728018-e315-44d3-88f3-12a0045cc5f3/d8p1wqo-3b4d78e8-db49-4a66-99c1-882a64c82be0.jpg/v1/fit/w_828,h_1148,q_70,strp/geralt_portrait_by_yamaorce_d8p1wqo-414w-2x.jpg?token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJ1cm46YXBwOjdlMGQxODg5ODIyNjQzNzNhNWYwZDQxNWVhMGQyNmUwIiwiaXNzIjoidXJuOmFwcDo3ZTBkMTg4OTgyMjY0MzczYTVmMGQ0MTVlYTBkMjZlMCIsIm9iaiI6W1t7ImhlaWdodCI6Ijw9MTIwMCIsInBhdGgiOiJcL2ZcL2FiNzI4MDE4LWUzMTUtNDRkMy04OGYzLTEyYTAwNDVjYzVmM1wvZDhwMXdxby0zYjRkNzhlOC1kYjQ5LTRhNjYtOTljMS04ODJhNjRjODJiZTAuanBnIiwid2lkdGgiOiI8PTg2NSJ9XV0sImF1ZCI6WyJ1cm46c2VydmljZTppbWFnZS5vcGVyYXRpb25zIl19.W1GcpQf9qlWAP_ivpHfJtdewYa4v9pndidCNdypYP80',
            inputShow: false,
            nickName: "",

            inputs: {
                inputNickName: null
            }
        }
    },

    methods: {
        async editProfile() {
            // When input is will be hidden
            if (this.inputShow == true) {
                const nickName = this.inputs.inputNickName.inputValue;
                
                if (this.nickName == nickName) {
                    this.inputShow = !this.inputShow;
                    return;
                }

                if (!nickName.match(/^[a-zA-Z0-9_]+$/))
                    return alert("Nickname can only contain 'a-Z', '0-9' and '_'");
                
                if (!await this.updateProfile(nickName))
                    return;
                
                this.nickName = nickName;
            }
            
            this.inputShow = !this.inputShow;
        },

        async updateProfile(nickName) {
            const result = await updateAccount(nickName);

            if (!result.response.ok)
                return alert(result.error);

            return true;
        },

        async logout() {
            await logoutAccount();

            sessionStorage.setItem('new_session', 'true');
            
            this.$router.push('/home');
        }
    },

    beforeMount() {
        const cookies = document.cookie.split(";");
        const nickName = cookies.find(x => x.includes("account_nickname")).split("=")[1];

        this.nickName = nickName;
    },

    mounted() {
        this.inputs.inputNickName = this.$refs['nick_name_input'];
    }
}
</script>

<template>
    <section class="flex absolute right-[0px] w-[310px] h-[calc(100vh-90px)] items-center flex-col bg-[#0D0D0D] bg-opacity-65">
        <h3 class="mt-20 text-[#D2B47C] text-7xl font-witcher-alternative">PROFILE</h3>
        <section class="mt-[50px] w-[75%] h-[400px] bg-[#151515]">
            <div class="flex items-center flex-col bg-[#302D2D] h-[128px]">
                <div class="image">
                    <img class="rounded-full object-cover mt-[44px] h-[140px] w-[140px] outline-8 outline outline-[#151515]" :src="image">
                </div>
            </div>
            <!-- NICK NAME -->
                <h1 v-show="!inputShow" :style="nickName.length > 15 ? 'font-size: 20px;' : 'font-size: 24px;'" class="text-center text-[#FFFFFF] mt-[70px] overflow-hidden text-ellipsis">{{nickName}}</h1>
                <CustomInput v-show="inputShow" class="custom-input" placeholder="NICKNAME" type="text" ref="nick_name_input" :valueInput="nickName" />
            <!-- NICK NAME -->
            <Button class="button m-auto mt-[35px]" @click="editProfile()" text="EDIT PROFILE" />
            <Button class="button m-auto mt-[5px]" @click="logout()" text="LOG OUT" />
        </section>
    </section>
</template>

<style scoped>
* :not(h3) {
    font-family: "Witcher";
}

.custom-input {
    display: flex;
    justify-content: center;
    align-items: center;
    width: 210px;
    height: 50px;
    
    font-size: 24px;

    margin-top: 65px;
    margin-bottom: -13px;
    padding-left: 20px;
    padding-right: 20px;
    padding-bottom: 5px;
}

.button {
    width: 208px;
    height: 60px;
    font-size: 24px;
    margin-top: 5px;
}
</style>