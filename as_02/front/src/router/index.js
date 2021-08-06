import VueRouter from 'vue-router'
import FilesNavigate from "@/components/txtFiles/FilesNavigate";
import StaffsPage from "@/components/StaffsDepartments/StaffsPage";
import DepartmentsPage from "@/components/StaffsDepartments/DepartmentsPage";


export default new VueRouter({
    mode:'history',
    routes:[
        {
            path:'/',
            component:FilesNavigate
        },
        {
            path:'/engFiles',
            component:FilesNavigate
        },
        {
            path:'/ruFiles',
            component:FilesNavigate
        },
        {
            path:'/digitFiles',
            component:FilesNavigate
        },
        {
            path:'/staffs',
            component:StaffsPage
        },
        {
            path:'/departments',
            component:DepartmentsPage
        }
    ]
})
