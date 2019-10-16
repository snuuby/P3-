import Login from './Login';

export const LoginConfig = {
    settings: {
        layout: {
            config: {}
        }
    },
    routes  : [
        {
            path     : '/login',
            component: Login
        }
    ]
};