import * as types from './mutation-types';

/**
 * Notify when an error occured
 * @param {*} context
 * @param {*} error
 */
export function notifyError(context, error) {
  context.commit(
    types.ERROR_HAPPENED,
    `${error.status}: ${error.responseText || error.statusText}`
  );
}

/**
 * Notify when an action is loading
 * @param {*} context
 * @param {*} isLoading
 */
export function notifyLoading(context, isLoading) {
  context.commit(types.SET_IS_LOADING, isLoading);
}

const errorMessage = error =>
  `${error.status}: ${error.responseText || error.statusText}`;

/**
 * Wraps the async call to an api service in order to handle loading, and errors.
 * @param {*} commit
 * @param {*} apiCall
 * @param {*} rethrowError
 */
async function wrapAsyncApiCall(commit, apiCall, rethrowError) {
  commit(types.SET_IS_LOADING, true);

  try {
    return await apiCall();
  } catch (error) {
    commit(types.ERROR_HAPPENED, errorMessage(error));

    if (rethrowError) throw error;
  } finally {
    commit(types.SET_IS_LOADING, false);
  }
}
