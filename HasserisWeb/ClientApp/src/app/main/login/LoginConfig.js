import Login from './LoginDialog';

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