import {
  AuthLayout,
  DefaultLayout,
} from '@/components/layouts';
// import {
//   UserList
// } from "@/components/admin/UserList";

export const publicRoute = [
  {
    path: '*',
    component: () => import(/* webpackChunkName: "errors-404" */ '@/views/error/NotFound.vue'),
  },
  {
    path: '/auth',
    component: AuthLayout,
    meta: {
      title: 'Login',
    },
    redirect: '/auth/login',
    hidden: true,
    children: [{
      path: 'login',
      name: 'login',
      meta: {
        title: 'Login',
      },
      component: () => import(/* webpackChunkName: "login" */ '@/views/auth/Login.vue'),
    }],
  },

  {
    path: '/404',
    name: '404',
    meta: {
      title: 'Not Found',
    },
    component: () => import(/* webpackChunkName: "errors-404" */ '@/views/error/NotFound.vue'),
  },

  {
    path: '/500',
    name: '500',
    meta: {
      title: 'Server Error',
    },
    component: () => import(/* webpackChunkName: "errors-500" */ '@/views/error/Error.vue'),
  },
  {
    path: '/403',
    name: 'Denegado',
    meta: {
      title: 'Acceso denegado',
      hiddenInMenu: true,
    },
    component: () => import(/* webpackChunkName: "error-403" */ '@/views/error/Deny.vue'),
  },
];

export const protectedRoute = [
  {
    path: '/',
    component: DefaultLayout,
    meta: {
      title: 'Home',
      group: 'apps',
      requiresAuth: true,
      icon: '',
    },
    redirect: '/dashboard',
    children: [{
      path: '/dashboard',
      name: 'Dashboard',
      meta: {
        title: 'Home',
        requiresAuth: true,
        group: 'apps',
        icon: 'dashboard',
      },
      component: () => import(/* webpackChunkName: "dashboard" */ '@/views/Home.vue'),
    },
    ],
  },

  // usuarios
  {
    name: 'Administracion',
    path: '/administracion',
    component: DefaultLayout,
    redirect: '/administracion/usuarios',
    meta: {
      title: 'Admin',
      icon: 'view_compact',
      group: 'admin',
    },
    children: [{
      path: '/administracion/usuarios',
      name: 'Usuarios',
      meta: {
        title: 'Usuarios',
        requiresAuth: true,
        roles: ['administrador'],
      },
      component: () => import(/* webpackChunkName: "table" */ '@/components/admin/UserList.vue'),
    }],
  },
  // nuevo usuario
  {
    name: 'nuevo',
    path: '/admin/usuarios',
    component: DefaultLayout,
    redirect: '/admin/usuarios/nuevo',
    meta: {
      title: 'Admin',
      icon: 'view_compact',
      group: 'admin',
    },
    children: [{
      path: '/admin/usuarios/nuevo',
      name: 'Nuevo Usuario',
      meta: {
        title: 'Nuevo Usuario',
        requiresAuth: true,
        roles: ['administrador'],
      },
      component: () => import(/* webpackChunkName: "table" */ '@/components/admin/NuevoUsuario.vue'),
    }],
  },
  // gestion de roles
  {
    name: 'gestionar-roles',
    path: '/admin/usuarios',
    component: DefaultLayout,
    redirect: '/admin/usuarios/gestionar-roles',
    meta: {
      title: 'Admin',
      icon: 'view_compact',
      group: 'admin',
    },
    children: [{
      path: '/admin/usuarios/gestionar-roles',
      name: 'Gestionar Roles',
      props: route => ({
        usuario: route.query.usuario,
      }),
      meta: {
        title: 'Gestionar Roles',
        requiresAuth: true,
        roles: ['administrador'],
      },
      component: () => import(/* webpackChunkName: "roles" */ '@/components/admin/GestionRoles.vue'),
    }],
  },
  // licencia
  {
    name: 'licencia',
    path: '/admin',
    component: DefaultLayout,
    redirect: '/admin/licencia',
    meta: {
      title: 'Licencia',
      icon: 'view_compact',
      group: 'admin',
    },
    children: [{
      path: '/admin/licencia',
      name: 'Licencia',
      meta: {
        title: 'Licencia',
        requiresAuth: true,
        roles: ['administrador'],
      },
      component: () => import(/* webpackChunkName: "roles" */ '@/components/admin/Licencia.vue'),
    }],
  },
  // configuracion
  {
    name: 'configuracion',
    path: '/admin',
    component: DefaultLayout,
    redirect: '/admin/configuracion',
    meta: {
      title: 'Admin',
      icon: 'view_compact',
      group: 'admin',
    },
    children: [{
      path: '/admin/configuracion',
      name: 'Configuracion',
      meta: {
        title: 'Configuracion',
        requiresAuth: true,
        roles: ['administrador'],
      },
      component: () => import(/* webpackChunkName: "roles" */ '@/components/admin/Configuracion.vue'),
    }],
  },
  // perfil de usuario
  {
    name: 'Account',
    path: '/account',
    component: DefaultLayout,
    redirect: '/account/perfil',
    meta: {
      title: 'PerfilDeUsuario',
      group: 'account',
    },
    children: [{
      path: '/account/perfil',
      name: 'PerfilDeUsuario',
      meta: {
        title: 'Perfil de Usuario',
        requiresAuth: true,
      },
      component: () => import(/* webpackChunkName: "table" */ '@/views/auth/PerfilDeUsuario.vue'),
    }],
  },
  // Contratacion
  {
    name: 'Contratacion',
    path: '/SGCont',
    component: DefaultLayout,
    redirect: '/SGCont/OfertasClientes',
    meta: {
      title: 'Contratos',
      group: 'SGCont',
    },
    children: [
      // {
      //   path: '/SGCont/dashboard',
      //   name: 'Dashboard',
      //   meta: {
      //     title: 'Cuadro de mando',
      //     requiresAuth: true,
      //   },
      //   component: () => import(/* webpackChunkName: "table" */ '@/views/SGCont/Dashboard.vue'),
      // },
      {
        path: '/SGCont/ContratosCliente',
        name: 'ContratosCliente',
        meta: {
          title: 'ContratosCliente',
          requiresAuth: true,
        },
        component: () => import(/* webpackChunkName: "table" */ '@/views/SGCont/ContratosCliente.vue'),
      },
      {
        path: '/SGCont/ContratosPrestador',
        name: 'ContratosPrestador',
        meta: {
          title: 'ContratosPrestador',
          requiresAuth: true,
        },
        component: () => import(/* webpackChunkName: "table" */ '@/views/SGCont/ContratosPrestador.vue'),
      },
      {
        path: '/SGCont/OfertasClientes',
        name: 'OfertasClientes',
        meta: {
          title: 'OfertasClientes',
          requiresAuth: true,
        },
        component: () => import(/* webpackChunkName: "table" */ '@/views/SGCont/OfertasClientes.vue'),
      },
      {
        path: '/SGCont/OfertasPrestador',
        name: 'OfertasPrestador',
        meta: {
          title: 'OfertasPrestador',
          requiresAuth: true,
        },
        component: () => import(/* webpackChunkName: "table" */ '@/views/SGCont/OfertasPrestador.vue'),
      },
      {
        path: '/SGCont/Config',
        name: 'Config',
        meta: {
          title: 'Config',
          requiresAuth: true,
        },
        component: () => import(/* webpackChunkName: "table" */ '@/views/SGCont/Config.vue'),
      },
      {
        path: '/SGCont/contrato/nuevo',
        name: 'Nuevo_Contrato',
        props: route => ({
          contrato: route.query.contrato,
        }),
        meta: {
          title: 'Nuevo_Contrato',
          requiresAuth: true,
        },
        component: () => import(/* webpackChunkName: "table" */ '@/components/SGCont/NuevoContrato.vue'),
      },
      {
        path: '/SGCont/contrato/detalles',
        name: 'Detalles_Contrato',
        props: route => ({
          contrato: route.query.contrato,
        }),
        meta: {
          title: 'Detalles_Contrato',
          requiresAuth: true,
        },
        component: () => import(/* webpackChunkName: "table" */ '@/components/SGCont/DetallesContrato.vue'),
      },
    ],
  },
];