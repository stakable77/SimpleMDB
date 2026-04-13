import { $, apiFetch, renderStatus, getQueryParam } from '/scripts/common.js';

(async function initActorMovieView() {
	const id = getQueryParam('id');
	const statusEl = $('#status');
	if (!id) return renderStatus(statusEl, 'err', 'Missing ?id in URL.');
	try {
		const am = await apiFetch(`/actors-movies/${encodeURIComponent(id)}`);
		$('#am-id').textContent = am.id;
		$('#am-actor-id').textContent = am.actorId;
		$('#am-movie-id').textContent = am.movieId;
		renderStatus(statusEl, 'ok', 'Link loaded successfully.');
	} catch (err) {
		renderStatus(statusEl, 'err', `Failed to load link ${id}: ${err.message}`);
	}
})();
