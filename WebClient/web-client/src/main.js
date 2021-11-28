import Vue from 'vue'
import Vuelidate from 'vuelidate'
import VueResource from 'vue-resource'
import App from './App.vue'
import router from './router'

import '@/assets/main.css'

Vue.config.productionTip = false

Vue.use(VueResource)
Vue.use(Vuelidate)
Vue.http.options.root = 'http://localhost:25459/'

new Vue({
  render: h => h(App),
  router
}).$mount('#app')
