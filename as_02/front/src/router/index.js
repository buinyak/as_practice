import VueRouter from 'vue-router'
import Navigate from "@/components/Navigate";


export default new VueRouter({
    mode:'history',
    routes:[
        {
            path:'/',
            component:Navigate
        },
        {
            path:'/engFiles',
            component:Navigate
        },
        {
            path:'/ruFiles',
            component:Navigate
        },
        {
            path:'/digitFiles',
            component:Navigate
        }
    ]
})
