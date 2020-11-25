import VueRouter from "vue-router";
import Login from "@/IMS/Views/Login.vue";
import HomePage from "@/IMS/Views/HomePage.vue";
import CreateUser from "@/IMS/Views/users/CreateUser.vue";
import EditUser from "@/IMS/Views/users/EditUser.vue";
import InventoryListPage from "@/IMS/Views/InventoryListPage.vue";
import CreateInventory from "@/IMS/Views/inventory/CreateInventory.vue";

const routePrefix = "inventory";

const routes = [
  {
    name: "home",
    path: `/${routePrefix}/home`,
    component: HomePage,
  },
  {
    name: "editUser",
    path: `/${routePrefix}/editUser/:id`,
    component: EditUser,
  },
  {
    name: "createInventory",
    path: `/${routePrefix}/createInventory`,
    component: CreateInventory,
  },
  {
    name: "create",
    path: `/${routePrefix}/create`,
    component: CreateUser,
  },
  {
    name: "login",
    path: `/${routePrefix}/login`,
    component: Login,
  },
  {
    name: "inventoryList",
    path: `/${routePrefix}/inventoryList`,
    component: InventoryListPage,
  },
  {
    path: "*",
    redirect: { name: "home" },
  },
];

export const router = new VueRouter({
  mode: "history",
  routes,
  linkActiveClass: "is-active",
});

router.beforeEach((to, from, next) => {
  const publicPages = [`/${routePrefix}/login`];
  const authRequired = !publicPages.includes(to.path);
  const loggedIn = localStorage.getItem("user");

  if (authRequired && !loggedIn) {
    return next(`/${routePrefix}/login`);
  }

  next();
});
