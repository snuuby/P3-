/**
 * Authorization Roles
 */
const authRoles = {
    admin    : ['admin'],
    adminPlus    : ['admin', 'adminPlus'],
    employee     : ['admin', 'adminPlus', 'employee'],
    onlyGuest: []
};

export default authRoles;
