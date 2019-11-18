/**
 * Authorization Roles
 */
const authRoles = {
    admin    : ['admin'],
    adminPlus    : ['admin', 'adminplus'],
    employee     : ['admin', 'adminplus', 'employee'],
    onlyGuest: []
};

export default authRoles;
