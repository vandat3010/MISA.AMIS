import Vue from 'vue'
import App from './App.vue'
import router from './router'
import "./style/basics/checkbox.css"
import 'vue-search-select/dist/VueSearchSelect.css'

Vue.config.productionTip = false

new Vue({
  router,
  render: h => h(App)
}).$mount('#app')