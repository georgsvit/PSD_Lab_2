<template>
<div class="flex-shrink-0" id="content">
    <form class="form-login" @submit.prevent="onFormSubmit">        
        <h1 class="h3 mb-3 fw-normal">Please sign in</h1>
            <input class="form-control"
                :class="{'is-invalid': $v.login.$error}" 
                @blur="$v.login.$touch()" 
                v-model="login" 
                type="email" 
                placeholder="Email" />                        

            <input class="form-control"
                :class="{'is-invalid': $v.password.$error}" 
                @blur="$v.password.$touch()" 
                v-model="password" 
                type="password" 
                placeholder="Password" />
        <div class="invalid-feedback h3 mb-3 fw-normal" v-if="!$v.login.required">Email field is required</div>
        <div class="invalid-feedback h3 mb-3 fw-normal" v-if="!$v.login.email">Email field is invalid</div>
        <div class="invalid-feedback h3 mb-3 fw-normal" v-if="!$v.password.required">Password is required</div>
            
        <button class="w-100 btn btn-lg btn-primary" type="submit">Sign In</button>            
    </form>        
    <div v-if="isVisible">
        <h1 v-if="!isSuccess">Unsuccess</h1>
        <h1 v-if="isSuccess">Success</h1>
    </div>
</div>         
</template>

<script>
import { email, alphaNum, required } from 'vuelidate/lib/validators'

export default {
    data () {
        return {         
            key: null,   
            resource: null,
            login: '',
            password: '',
            isLoading: false,
            isSuccess: false,
            isVisible: false
        }
    },
    validations: {
        login: {
            email,
            required
        },
        password: {
            alphaNum,
            required
        }
    },
    methods: {    
        onFormSubmit() {
            if (this.password === '' || this.login === '') {
                return false
            }

            var data = this.key.encrypt(this.login + " " + this.password, 'base64')
            console.log(data)
            console.log(this.login + " " + this.password)

            this.resource.save(null, {'data': data})
                .then(response => response.json())
                .then(json => {
                    console.log(json)
                    if (json == true) {                    
                        this.isSuccess = true
                    } else {
                        this.isSuccess = false
                    }
                    this.isVisible = true
                })
        },
    },

    created() {
        var rsa = require("node-rsa")
        this.resource = this.$resource('login')

        this.resource.get().then(response => response.json()).then(json => {
            if (json !== null) {
                this.key = new rsa(json)                
                console.log(this.key.exportKey("public"))
            }                
        })
    }
}
</script>