import axios from 'axios';
import jwtDecode from 'jwt-decode';
import FuseUtils from '@fuse/FuseUtils';
class jwtService extends FuseUtils.EventEmitter {

    init()
    {
        this.setInterceptors();
        this.handleAuthentication();
    }

    setInterceptors = () => {
        axios.interceptors.response.use(response => {
            return response;
        }, err => {
            return new Promise((resolve, reject) => {
                if ( err.response.status === 401 && err.config && !err.config.__isRetryRequest )
                {
                    // if you ever get an unauthorized response, logout the user
                    this.emit('onAutoLogout', 'Invalid access_token');
                    this.setSession(null);
                }
                throw err;
            });
        });
    };

    handleAuthentication = () => {

        let access_token = this.getAccessToken();

        if ( !access_token )
        {
            this.emit('onNoAccessToken');

            return;
        }

        if ( this.isAuthTokenValid(access_token) )
        {
            this.setSession(access_token);
            this.emit('onAutoLogin', true);
        }
        else
        {
            this.setSession(null);
            this.emit('onAutoLogout', 'access_token expired');
        }
    };

    createUser = (data) => {
        return new Promise((resolve, reject) => {
            axios.post('/api/auth/register', data)
                .then(response => {
                    if ( response.data.user )
                    {
                        this.setSession(response.data.access_token);
                        resolve(response.data.user);
                    }
                    else
                    {
                        reject(response.data.error);
                    }
                });
        });
    };

    signInWithEmailAndPassword = (username, password) => {
        const user = { name: username, pass: password };
        return new Promise((resolve, reject) => {
            axios.post('auth/verify', user).
            then(response => {
                if ( response.data.user )
                {

                    const tempuser = {
                        uid: response.data.user.ID,
                        from: 'database',
                        role: [response.data.user.Type],
                        data: {
                            photoURL: response.data.user.PhotoPath,
                            displayName: response.data.user.Username,
                            email: response.data.user.ContactInfo.Email,
                            firstName: response.data.user.Firstname,
                            lastName: response.data.user.Lastname
                        }
                    }
                    this.setSession(response.data.access_token);
                    resolve(tempuser);
                }
                else
                {
                    const tempError = { username: username, password: password, msg: response.data.error};
                    reject(tempError);
                }
            });
        });
    };

    signInWithToken = () => {
        const data = { access_token: this.getAccessToken() };
        return new Promise((resolve, reject) => {
            axios.post('auth/AccessToken', data
            )
                .then(response => {
                    if ( response.data.user )
                    {
                        const tempuser = {
                            uid: response.data.user.ID,
                            from: 'database',
                            role: [response.data.user.Type],
                            data: {
                                photoURL: response.data.user.PhotoPath,
                                displayName: response.data.user.Username,
                                email: response.data.user.ContactInfo.Email,
                                firstName: response.data.user.Firstname,
                                lastName: response.data.user.Lastname
                            }
                        }
                        this.setSession(response.data.access_token);
                        resolve(tempuser);
                    }
                    else
                    {
                        this.logout();
                        reject('Failed to login with token.');
                    }
                })
                .catch(error => {
                    this.logout();
                    reject('Failed to login with token.');
                });
        });
    };

    updateUserData = (user) => {
        return axios.post('/api/auth/user/update', {
            user: user
        });
    };

    setSession = access_token => {
        if ( access_token )
        {
            localStorage.setItem('jwt_access_token', access_token);
            axios.defaults.headers.common['Authorization'] = 'Bearer ' + access_token;
        }
        else
        {
            localStorage.removeItem('jwt_access_token');
            delete axios.defaults.headers.common['Authorization'];
        }
    };

    logout = () => {
        this.setSession(null);
    };

    isAuthTokenValid = access_token => {
        if ( !access_token )
        {
            return false;
        }
        const decoded = jwtDecode(access_token);
        const currentTime = Date.now() / 10000;
        if ( decoded.exp < currentTime )
        {
            console.warn('access token expired');
            return false;
        }
        else
        {
            return true;
        }
    };

    getAccessToken = () => {
        return window.localStorage.getItem('jwt_access_token');
    };
}

const instance = new jwtService();

export default instance;
